<script setup lang="ts">
import { CreateCalendarDTO } from "@/generated/DTO";
import { calendarApiSymbol } from "@/injectionKeys";
import {CalendarBrief, CalendarBriefMapper} from "@/models/CalendarBrief";
import { inject, ref } from "vue";

const calendarBriefs = ref<CalendarBrief[]>([]);
const calendarName = ref("");
const calendarApi = inject(calendarApiSymbol, () => {throw new Error()}, true);

function nameChanged(newName: string) {
	calendarName.value = newName;
}

function addCalendar() {
	calendarApi.CreateCalendar(new CreateCalendarDTO(calendarName.value));
}

calendarApi.GetCalendarList()
	.then((dtos) => {
		calendarBriefs.value = dtos.map(x => 
			CalendarBriefMapper.CalendarBriefDtoToModel(x)
		);
	});
</script>

<template>
	<div class="home">
		<v-btn @click="addCalendar"></v-btn>
		<v-text-field :value="calendarName"
					  @input="nameChanged"></v-text-field>
		<ul>
			<li v-for="calendarBrief in calendarBriefs"
				:key="calendarBrief.Guid">
				{{calendarBrief.Name}} <a :href="'/calendars/' + calendarBrief.Guid"> Visit</a>
			</li>
		</ul>
	</div>
</template>