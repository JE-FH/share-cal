<script lang="ts" setup>
import { CreateEventDTO } from '@/generated/DTO';
import { calendarApiSymbol } from '@/injectionKeys';
import { inject, ref, defineProps } from 'vue';

import SCDateTimePicker from './SCDateTimePicker.vue';

const calendarApi = inject(calendarApiSymbol, () => {throw new Error("CalendarApi is required")}, true);

const props = defineProps<{
	calendarGuid: string;
}>();

const startDateTime = ref(new Date());

function startDateTimeChanged(newDate: Date) {
	console.log(newDate);
	if (endDateTime.value.getTime() < newDate.getTime()) {
		endDateTime.value = newDate;
	}
	startDateTime.value = newDate;
}

const endDateTime = ref(new Date());

function endDateTimeChanged(newDate: Date) {
	console.log(newDate);
	endDateTime.value = newDate;
}

const summary = ref("");

const loading = ref(false);

function addEvent() {
	loading.value = true;
	calendarApi.CreateEvent(new CreateEventDTO(
		startDateTime.value,
		endDateTime.value,
		summary.value,
		"",
		"",
		[props.calendarGuid]
	))
		.then(() => {
			loading.value = false;
		});
}
</script>

<template>
	<v-card>
		<v-card-title>
			Add event
		</v-card-title>
		<v-card-text>
				<v-row no-gutters>
					<v-col no-gutters cols="12">
						<v-text-field label="Summary" v-model="summary" :disabled="loading"/>
					</v-col>
				</v-row>
				<v-row no-gutters align="center">
					<v-col no-gutters>
						<v-row no-gutters>
							<SCDateTimePicker :dateTime="startDateTime" @change="startDateTimeChanged" :disabled="loading"/>
						</v-row>
					</v-col>
					<v-col cols="1" align="center">
						To
					</v-col>
					<v-col no-gutters>
						<v-row no-gutters>
							<SCDateTimePicker :earliestDateTime="startDateTime" :dateTime="endDateTime" @change="endDateTimeChanged" :disabled="loading"/>
						</v-row>
					</v-col>
				</v-row>
				<v-row no-gutters justify="center">
					<v-btn no-gutters color="primary" @click="addEvent" :disabled="loading" :loading="loading">Add event</v-btn>
				</v-row>
		</v-card-text>
	</v-card>
</template>