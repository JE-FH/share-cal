class Transpiler
{
    private HashSet<Type> _dtos;
    
    public Transpiler()
    {
        _dtos = new HashSet<Type>();
    }

    public Transpiler AddDto(Type type)
    {
        _dtos.Add(type);
        return this;
    }

    public string TranspileToTypescript()
    {
        var lines = new List<string>();

        foreach (var dto in _dtos)
        {
            lines.Add($"export class {dto.Name} {{");
            
            foreach (var field in dto.GetFields())
            {
                var type = field.FieldType.UnderlyingSystemType;
                lines.Add($"\t{field.Name}: {GetTypescriptType(type)};");
            }

            lines.Add("\tconstructor(");
            
            foreach (var field in dto.GetFields())
            {
                var type = field.FieldType.UnderlyingSystemType;
                lines.Add($"\t\t_{field.Name}: {GetTypescriptType(type)},");
            }
            lines.Add("\t) {");
            
            foreach (var field in dto.GetFields())
            {
                lines.Add($"\t\tthis.{field.Name} = _{field.Name};");
            }
            
            lines.Add("\t}");
            
            lines.Add("\tstatic FromObject(obj: Record<any, any>) {");

            foreach (var field in dto.GetFields())
            {
                var type = field.FieldType.UnderlyingSystemType;
                lines.Add($"\t\tif (!({GetTypescriptTypeAssertion(type, $"obj.{field.Name}")}))");
                lines.Add($"\t\t\tthrow new Error('DTO Type mismatch, expected {field.Name} to be {GetTypescriptType(type)} but got ' + typeof(obj.{field.Name}));");
            }
            
            lines.Add($"\t\treturn new {dto.Name}(");
            foreach (var field in dto.GetFields())
            {
                var type = field.FieldType.UnderlyingSystemType;
                var converter = GetTypescriptConverter(type);
                if (converter == null)
                    lines.Add($"\t\t\tobj.{field.Name},");
                else
                    lines.Add($"\t\t\t{converter}(obj.{field.Name}),");
            }
            lines.Add($"\t\t);");
            
            lines.Add("\t}");

            lines.Add("}");
        }

        
        return String.Join("\n", lines);
    }

    string GetTypescriptType(Type type)
    {
        if (typeof(Guid) == type)
            return "string";
        if (typeof(string) == type)
            return "string";
        if (typeof(DateTime) == type)
            return "Date";
        if (typeof(int) == type)
            return "number";
        if (typeof(float) == type)
            return "number";
        if (typeof(double) == type)
            return "number";
        if (type.IsGenericType && typeof(List<int>).GetGenericTypeDefinition() == type.GetGenericTypeDefinition())
            return $"Array<{GetTypescriptType(type.GetGenericArguments()[0])}>";
        if (_dtos.Contains(type))
            return type.Name;
        Console.WriteLine($"WARNING: Could not translate type {type}, defaulting to any");
        return "any";
    }
    
    string? GetTypescriptConverter(Type type)
    {
        if (typeof(Guid) == type)
            return null;
        if (typeof(string) == type)
            return null;
        if (typeof(DateTime) == type)
            return "new Date";
        if (typeof(int) == type)
            return null;
        if (typeof(float) == type)
            return null;
        if (typeof(double) == type)
            return null;
        if (type.IsGenericType && typeof(List<int>).GetGenericTypeDefinition() == type.GetGenericTypeDefinition())
        {
            var innerConverter = GetTypescriptConverter(type.GenericTypeArguments[0]);
            if (innerConverter != null)
                return $"((x) => x.map(y => {innerConverter}(y)))";
            return null;
        }
        if (_dtos.Contains(type))
            return $"{type.Name}.FromObject";
;       Console.WriteLine($"WARNING: Could not translate type {type}, defaulting to any");
        return "any";
    }

    string GetTypescriptTypeAssertion(Type type, string name)
    {
        if (typeof(Guid) == type)
            return $"typeof({name}) == 'string'";
        if (typeof(string) == type)
            return $"typeof({name}) == 'string'";
        if (typeof(DateTime) == type)
            return $"!Number.isNaN(new Date({name}).getTime())";
        if (typeof(int) == type)
            return $"Number.isInteger({name})";
        if (typeof(float) == type)
            return $"!Number.isNaN({name})";
        if (typeof(double) == type)
            return $"!Number.isNaN({name})";
        if (type.IsGenericType && typeof(List<int>).GetGenericTypeDefinition() == type.GetGenericTypeDefinition())
            return $"{name} instanceof Array && ({name} as Array<any>).every(x => ({GetTypescriptTypeAssertion(type.GetGenericArguments()[0], "x")}))";
        if (_dtos.Contains(type))
        {
            return String.Join("&&", type.GetFields().Select(field =>
            {
                return $"({GetTypescriptTypeAssertion(field.FieldType.UnderlyingSystemType, $"{name}.{field.Name}")})";
            }));
        }
        Console.WriteLine($"WARNING: Could not translate type {type}, defaulting to any");
        return "true";
    }
}