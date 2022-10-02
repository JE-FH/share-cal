<script setup lang="ts">
import { DateOnly, TimeOnly } from '@/Helpers/DateTimeHelper';
import { dateTimeHelperSymbol } from '@/injectionKeys';
import { computed, inject, defineProps, defineEmits } from 'vue';
import SCDatePicker from './SCDatePicker.vue';
import SCTimePicker from './SCTimePicker.vue';

const dateHelper = inject(dateTimeHelperSymbol, () => {throw new Error("IDateHelper is required")}, true);

const props = defineProps<{
	dateTime: Date;
	earliestDateTime?: Date;
}>();

const emit = defineEmits<{
	(e: "change", newDate: Date): void;
}>();

const time = computed<TimeOnly>(() => {
	return dateHelper.ExtractTime(props.dateTime);
});

const date = computed<DateOnly>(() => {
	return dateHelper.ExtractDate(props.dateTime);
});

function dateChanged(newDate: DateOnly) {
	console.log("change date")
	emit("change", dateHelper.ToDate(newDate, time.value));
}

function timeChanged(newTime: TimeOnly) {
	emit("change", dateHelper.ToDate(date.value, newTime));
}

const earliestTime = computed<TimeOnly | undefined>(() => {
	if (props.earliestDateTime == null)
		return undefined;

	if (dateHelper.IsSameDate(
		dateHelper.ExtractDate(props.dateTime), 
		dateHelper.ExtractDate(props.earliestDateTime)
	)) {
		return dateHelper.ExtractTime(props.earliestDateTime);
	}
	return undefined;
});


const earliestDate = computed<DateOnly | undefined>(() => {
	if (props.earliestDateTime == null)
		return undefined;

	return dateHelper.ExtractDate(props.earliestDateTime)
});
</script>

<template>
	<v-row>
		<v-col cols="6" no-gutters>
			<SCDatePicker :earliestDate="earliestDate" :date="date" label="Date" @change="dateChanged"/>
		</v-col>
		<v-col cols="6" no-gutters>
			<SCTimePicker :earliestTime="earliestTime" :time="time" label="Time" @change="timeChanged"/>
		</v-col>
	</v-row>
	
</template>