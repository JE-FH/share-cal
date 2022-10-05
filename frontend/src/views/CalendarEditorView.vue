<script setup lang="ts">
import SCCalendar, { CalendarSource, CalendarSourceType } from '../components/SCCalendar.vue';
import {defineProps, ref} from "vue";
import AddEventDialog from '@/components/AddEventDialog.vue';

const props = defineProps<{
	guid: string;
}>();

const calendarSource = ref<CalendarSource>({
	type: CalendarSourceType.SingleCalendar,
	guid: props.guid
});

</script>

<template>
	<v-container>
		<v-row>
			<v-col cols="4">
				<AddEventDialog :calendarGuid="props.guid"/>
			</v-col>
		</v-row>
		<v-row>
			<v-col cols="12">
				<SCCalendar :source="calendarSource"/>
			</v-col>
		</v-row>
	</v-container>
</template>
