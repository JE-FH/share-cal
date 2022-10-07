<script setup lang="ts">
	import { provide } from 'vue';
	import { CalendarAPI } from './Helpers/CalendarAPI';
	import { DateTimeHelper } from './Helpers/DateTimeHelper';
	import { calendarApiSymbol, dateTimeHelperSymbol } from './injectionKeys';
	
	provide(calendarApiSymbol, new CalendarAPI());
	provide(dateTimeHelperSymbol, new DateTimeHelper());
</script>

<template>
	<v-app>
		<router-view></router-view>
	</v-app>
</template>

