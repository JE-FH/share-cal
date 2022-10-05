
<script lang="ts">
import { TimeOnly } from "@/Helpers/DateTimeHelper";

function decodeTimePickerTime(timeStr: string): TimeOnly {
	let split = timeStr.split(":");
	return {
		hour: Number(split[0]),
		minute: Number(split[1]),
		seconds: split.length > 2 ? Number(split[2]) : 0
	};
}

</script>

<script setup lang="ts">
import { ref, defineProps, defineEmits, computed, inject } from 'vue';
import { dateTimeHelperSymbol } from "@/injectionKeys";

const dateHelper = inject(dateTimeHelperSymbol, () => {throw new Error("IDateHelper is required")}, true);

const props = defineProps<{
	time: TimeOnly;
	label?: string;
	prependIcon?: string;
	earliestTime?: TimeOnly;
	disabled?: boolean;
}>();

const emit = defineEmits<{
	(e: "change", newDate: TimeOnly): void;
}>();

const unconfirmedTime = ref<TimeOnly>({
	hour: props.time.hour,
	minute: props.time.minute,
	seconds: props.time.seconds
});

const menu = ref(false);

const ClockFormat = computed(() => {
	return dateHelper.UsesAmPm ? "ampm" : "24hr"
})

function timePickerChanged(newTime: string) {
	console.log(newTime);
	unconfirmedTime.value = decodeTimePickerTime(newTime);
}

function confirmTime() {
	console.log(JSON.parse(JSON.stringify(unconfirmedTime.value)));
	emit("change", {
		hour: unconfirmedTime.value.hour,
		minute: unconfirmedTime.value.minute,
		seconds: unconfirmedTime.value.seconds
	});
	menu.value = false;
}

function encodeTimePickerTime(time: TimeOnly): string {
	return `${time.hour.toString().padStart(2, '0')}:${time.minute.toString().padStart(2, '0')}:${time.seconds.toString().padStart(2, '0')}`;
}

function closeAndReset() {
	unconfirmedTime.value = {
		hour: props.time.hour,
		minute: props.time.minute,
		seconds: props.time.seconds
	};

	menu.value = false;
}

function onMenuChange(isOpen: boolean) {
	if (!isOpen) {
		closeAndReset();
	}
}

</script>

<template>
	<v-menu
		v-model="menu"
		:close-on-content-click="false"
		transition="scale-transition"
		offset-y
		min-width="auto"
		@input="onMenuChange"
		:label="props.label"
		:disabled="props.disabled"
	>
		<template v-slot:activator="{ on, attrs }">
			<v-text-field
				:label="props.label"
				:prepend-icon="props.prependIcon"
				readonly
				v-bind="attrs"
				v-on="on"
				:value="dateHelper.FormatTimeOnly(props.time)"
				:disabled="props.disabled" />
		</template>
		<v-time-picker
			:format="ClockFormat"
			scrollable
			@input="timePickerChanged"
			:value="encodeTimePickerTime(unconfirmedTime)"
			:min="props.earliestTime != null ? encodeTimePickerTime(props.earliestTime) : undefined"
			:disabled="props.disabled"
			>
			<v-spacer></v-spacer>
			<v-btn
				color="primary"
				text
				@click="closeAndReset()"
			>
				Cancel
			</v-btn>
			<v-btn
				color="primary"
				text
				@click="confirmTime"
			>
				OK
			</v-btn>
		</v-time-picker>
	</v-menu>
</template>