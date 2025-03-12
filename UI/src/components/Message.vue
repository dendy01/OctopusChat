<template>
	<div :class="['message-user', message.UserId === currentUserId ? 'user-friend' : '']">
        <Avatar :text="members[message.UserId]" />

		<div class="message-user__item">
	        <div class="item-text">
                <h3 class="message-user__text">{{ message.Text }}</h3>   
            </div>
	        <p class="item-time">{{ message.CreatedDateTime }}</p>
	    </div>
	</div>
</template>

<script setup lang="ts">
import Avatar from "./UI/Avatar.vue";
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
    margin-top: 24px;
    display: flex;
    gap: 12px;

    .message-user__item {
        width: 40%;

        .item-text {
            padding: 16px;
            border-radius: 0 12px 12px 12px;

            color: #433c50;
            background-color: #fff;
            box-shadow: 0 2px 2px rgba(0, 0, 0, .3);
        }

        .item-time {
            margin-top: 8px;
            color: #ABA8B1;
        }

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

    flex-direction: row-reverse;
    justify-content: flex-start;

    .message-user__item {

        .item-text {
            padding: 16px;
            border-radius: 12px 0 12px 12px;
            
            color: #fff;
            background-color: #8c57ff;
        }
    }
}
</style>