import { defineStore } from "pinia";

export const useStore = defineStore("Calendar", {
    state: () => ({count: 0}),
    actions: {
        increment() {
            this.count++;
        }
    }
})