<template>
    <header class="header">
        <div class="header-radio">
            <div
                v-if="messages?.Members"
                class="header-radio__item"
                v-for="[id, name] in Object.entries(messages?.Members)"
                :key="id"
            >
                <input 
                    type="radio" 
                    name="selectUser"
                    :id="name"
                    :value="id"
                    @input="onInput($event.target.value)"
                >
                <label :for="name">{{ name }}</label>
            </div>
        </div>
    </header>
</template>

<script setup lang="ts">
import MessageModel from "./Models/MessageModel.ts";

interface IPropsType {
    messages: MessageModel;
    currentUser: number;
}

interface IEmitsType {
    (e: 'update:modelValue', value: string): void;
}

defineProps<IPropsType>();
const emit = defineEmits<IEmitsType>();

const onInput = (value: string) => {
    emit('update:modelValue', value);
};
</script>

<style scoped>
.header {
    height: 75px;
    min-height: 75px;

    background-color: var(--color-white-400);
    border-bottom: 1px solid var(--color-gray-700);
}
</style>