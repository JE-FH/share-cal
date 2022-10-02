<script lang="ts">
function parseDatePickerDate(date: string): DateOnly {
	let split = date.split("-");
	return {
		year: Number(split[0]),
		month: Number(split[1]),
		day: Number(split[2])
	};
}
</script>

<script setup lang="ts">
import { DateOnly } from '@/Helpers/DateTimeHelper';
import { dateTimeHelperSymbol } from '@/injectionKeys';
import { ref, defineProps, defineEmits, inject, unref, toRaw, markRaw } from 'vue';
import { VMenu } from 'vuetify/lib/components';

const dateHelper = inject(dateTimeHelperSymbol, () => {throw new Error("IDateHelper is required")}, true);

const props = defineProps<{
	date: DateOnly;
	label?: string;
	prependIcon?: string;
	earliestDate?: DateOnly;
}>();

const emit = defineEmits<{
	(e: "change", newDate: DateOnly): void;
}>();

const menu = ref(false);

const unconfirmedDate = ref<DateOnly>({
	year: props.date.year,
	month: props.date.month,
	day: props.date.day
});

function encodeDatePickerDate(date: DateOnly): string {
	return `${date.year.toString().padStart(2, '0')}-${date.month.toString().padStart(2, '0')}-${date.day.toString().padStart(2, '0')}`;
}

function dateConfirm() {
	menu.value = false;
	emit("change", {
		year: unconfirmedDate.value.year,
		month: unconfirmedDate.value.month,
		day: unconfirmedDate.value.day
	});
}

function datePickerChanged(dateString: string) {
	unconfirmedDate.value = parseDatePickerDate(dateString);
}

</script>

<template>
	<v-menu
		v-model="menu"
		:close-on-content-click="false"
		transition="scale-transition"
		offset-y
		min-width="auto"
		:label="label"
	>
		<template v-slot:activator="{ on, attrs }">
			<v-text-field
				:prepend-icon="props.prependIcon"
				readonly
				v-bind="attrs"
				v-on="on"
				:label="props.label"
				:value="dateHelper.FormatDateOnly(props.date)"
			></v-text-field>
		</template>
		<v-date-picker
			scrollable
			@change="datePickerChanged"
			:value="encodeDatePickerDate(unconfirmedDate)"
			:min="props.earliestDate != null ? encodeDatePickerDate(props.earliestDate) : undefined"
		>
			<v-spacer></v-spacer>
			<v-btn
				text
				color="primary"
				@click="menu = false"
			>
				Cancel
			</v-btn>
			<v-btn
				text
				color="primary"
				@click="dateConfirm"
			>
				OK
			</v-btn>
		</v-date-picker>
	</v-menu>
</template>