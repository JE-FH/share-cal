<script setup lang="ts">
import SCCalendar, { CalendarSource, CalendarSourceType } from '../components/SCCalendar.vue';
import SCDatePicker from '../components/SCDatePicker.vue';
import {defineProps, ref} from "vue";
import { DateOnly } from '@/Helpers/DateHelper';

const props = defineProps<{
	guid: string;
}>();

const calendarSource = ref<CalendarSource>({
	type: CalendarSourceType.SingleCalendar,
	guid: props.guid
});

let now = new Date();

const startDate = ref<DateOnly>({
	year: now.getFullYear(),
	month: now.getMonth() + 1,
	day: now.getDate()
});

function startDateChanged(newDate: DateOnly) {
	console.log(newDate);
	startDate.value = newDate;
}
</script>

<template>
	<v-container>
		<v-row>
			<v-col cols="4">
				<v-card>
					<v-card-title>
						Add event
					</v-card-title>
					<v-card-text>
						<v-text-field label="Summary" />
						<SCDatePicker label="Date" :date="startDate" @change="startDateChanged"/>
					</v-card-text>
				</v-card>
			</v-col>
		</v-row>
		<v-row>
			<v-col cols="12">
				<SCCalendar :source="calendarSource"/>
			</v-col>
		</v-row>
	</v-container>
</template>
