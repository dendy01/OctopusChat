<template>
	<aside>
		<div class="chat-contacts">
            <div class="chat-contacts__user contacts-active">
                <div class="user-icon">
                    <img src="../assets/user_icon.jfif">
                </div>
                <div class="user-name">
                    <h3 class="name">{{ messages?.Members[messages?.Messages.at(-1).UserId] }}</h3>
                    <p class="last-message">{{ messages?.Messages.at(-1).Text }}</p>
                </div>
            </div>
        </div>
	</aside>
</template>

<script setup lang="ts">
import MessageModel from "./Models/MessageModel.ts";

interface IPropsType {
	messages: MessageModel;
}

defineProps<IPropsType>();
</script>

<style setup>
.chat-contacts {
    width: 400px;
    height: 100%;

    position: fixed;
    top: 0;
    left: 0;

    border-right: 1px solid #FFB433;
    background-color: #FBF8EF;

    .chat-contacts__user {
        padding: 8px;

        display: flex;
        align-items: center;
        gap: 20px;
    }

    .chat-contacts__user:hover {
        background-color: #D5E5D5;
    }

    .user-icon {
        width: 60px;
        height: 60px;
        border-radius: 50%;
        overflow: hidden;
    }

    .last-message {
        margin-top: 4px;
    }

    .contacts-active {
        color: #fff;
        background-color: #3674B5;
    }

    .contacts-active:hover {
        background-color: #3674B5;
    }
}
</style>