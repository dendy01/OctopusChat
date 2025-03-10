<template>
	<div :class="['message-user', message.UserId === currentUserId ? 'user-friend' : '']">
		<div class="message-user__item">
	        <h3 class="message-user__name">{{ members[message.UserId] }}</h3>
	        <p class="message-user__text">{{ message.Text }}</p>
	        <p>{{ message.DateTime }}</p>
	    </div>
	</div>
</template>

<script setup lang="ts">
import { MessageModel } from "./Models/MessageModel.ts";
import { computed } from "vue";

interface IPropsType {
	message: MessageModel;
	members: { [id: number]: string }
}

defineProps<IPropsType>();

const currentUserId = Number(document.cookie.split('=')[1]);
</script>

<style scoped>
.message-user {
    .message-user__item {
        width: 40%;
        padding: 16px;
        margin-top: 8px;
        border-radius: 12px;

        background-color: #D5E5D5;

        .message-user__text {
            margin: 8px 0;
            font-size: 18px;
        }
    }
}

.message-user:first-child {
    margin-top: 0;
}

.user-friend {
    text-align: right;

    display: flex;
    justify-content: flex-end;
}
</style>