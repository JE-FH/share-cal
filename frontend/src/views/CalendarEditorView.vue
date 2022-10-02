<script setup lang="ts">
import SCCalendar, { CalendarSource, CalendarSourceType } from '../components/SCCalendar.vue';
import SCDatePicker from '../components/SCDatePicker.vue';
import {defineProps, ref} from "vue";
import { DateOnly, TimeOnly } from '@/Helpers/DateTimeHelper';
import SCTimePicker from '@/components/SCTimePicker.vue';
import SCDateTimePicker from '@/components/SCDateTimePicker.vue';

const props = defineProps<{
	guid: string;
}>();

const calendarSource = ref<CalendarSource>({
	type: CalendarSourceType.SingleCalendar,
	guid: props.guid
});

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
						<v-container>
							<v-row no-gutters>
								<v-col no-gutters cols="12"><v-text-field label="Summary" /></v-col>
							</v-row>
							<v-row no-gutters align="center">
								<v-col no-gutters>
									<v-container>
										<v-row no-gutters>
											<SCDateTimePicker :dateTime="startDateTime" @change="startDateTimeChanged"/>
										</v-row>
									</v-container>
								</v-col>
								<v-col cols="1" align="center">
									To
								</v-col>
								<v-col no-gutters>
									<v-container>
										<v-row no-gutters>
											<SCDateTimePicker :earliestDateTime="startDateTime" :dateTime="endDateTime" @change="endDateTimeChanged"/>
										</v-row>
									</v-container>
								</v-col>
							</v-row>
							<v-row no-gutters justify="center">
								<v-btn no-gutters color="primary">Add event</v-btn>
							</v-row>
						</v-container>
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
