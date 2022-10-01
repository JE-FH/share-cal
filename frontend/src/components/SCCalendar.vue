<script lang="ts">
export enum CalendarSourceType {
	SingleCalendar,
	CombinedCalendar
}

export interface CalendarSource {
	type: CalendarSourceType,
	guid: string
}
</script>

<script setup lang="ts">
import { calendarApiSymbol } from '@/injectionKeys';
import { CalendarView, CalendarViewMapper } from '@/models/CalendarView';
import { reactive, ref, defineProps, inject } from 'vue';
import { CalendarEvent } from 'vuetify';

const props = defineProps<{
	source: CalendarSource
}>();

const events = ref<CalendarEvent[]>([]);
const loaded = ref(false);

const calendarApi = inject(calendarApiSymbol, () => {throw new Error("CalendarApi is required")}, true);

async function GetEvents(): Promise<CalendarView> {
	if (props.source.type == CalendarSourceType.SingleCalendar) {
		return await GetSingleCalendar();
	} else {
		throw new Error();
	}
}

async function GetSingleCalendar(): Promise<CalendarView> {
	let dto = await calendarApi.GetCalendar(props.source.guid)
	if (dto == null) {
		throw new Error("Got null");
	}
	return CalendarViewMapper.CalendarViewDtoToModel(dto);
}

GetEvents()
	.then((view) => {
		return view.Events.map(event => (reactive({
			start: event.Start,
			end: event.End,
			name: event.Summary,
			timed: true
		})))
	})
	.then((_events) => {
		events.value = _events;
		loaded.value = true;
	})

</script>


<template>
	<div>
		<v-progress-linear v-if="!loaded" indeterminate />
		<v-calendar type="week" 
					event-overlap-mode="stack" 
					:events="events" />
	</div>
</template>