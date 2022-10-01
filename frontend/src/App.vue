<script setup lang="ts">
	import { provide } from 'vue';
	import { CalendarAPI } from './Helpers/CalendarAPI';
	import { DateHelper } from './Helpers/DateHelper';
	import { calendarApiSymbol, dateHelperSymbol } from './injectionKeys';
	
	provide(calendarApiSymbol, new CalendarAPI());
	provide(dateHelperSymbol, new DateHelper());
</script>


<template>
	<v-app>
		<router-view></router-view>
	</v-app>
</template>

