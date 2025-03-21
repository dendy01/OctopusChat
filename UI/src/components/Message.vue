<template>
	<div :class="['message-user', message.UserId === currentUserId ? 'user-friend' : '']">
        <Avatar :text="members[message.UserId]" />

		<div class="message-user__item">
	        <div class="item-text">
                <p class="message-user__text">{{ message.Text }}</p>   
            </div>
	        <p class="item-time">{{ getDate() }}</p>
	    </div>
	</div>
</template>

<script setup lang="ts">
import Avatar from "./UI/Avatar.vue";
import { MessageModel } from "./Models/MessageModel.ts";

interface IPropsType {
	message: MessageModel;
	members: { [id: number]: string }
}

const props = defineProps<IPropsType>();

const currentUserId = Number(document.cookie.split('=')[1]);

const getDate = () => {
    const hours = props.message.CreatedDateTime.split('T')[1].split(":")[0];
    const minutes = props.message.CreatedDateTime.split('T')[1].split(":")[1];

    return `${ hours }:${ minutes }`;
}
</script>

<style scoped>
.message-user {
    margin-top: var(--size-xl);
    display: flex;
    gap: var(--size-md);

    .message-user__item {
        width: 40%;

        .item-text {
            padding: var(--size-sm) var(--size-lg);
            border-radius: 0 var(--size-md) var(--size-md) var(--size-md);

            color: var(--color-dark-700);
            background-color: var(--color-white-700);
            box-shadow: 0 2px 2px rgba(0, 0, 0, .3);
        }

        .item-time {
            font-size: var(--fs-sm);
            margin-top: var(--size-sm);
            color: var(--color-gray-400);
        }

        .message-user__text {
            font-size: var(--fs-md);
            font-weight: 500;
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
            padding: var(--size-sm) var(--size-lg);
            border-radius: var(--size-md) 0 var(--size-md) var(--size-md);
            
            color: var(--color-white-700);
            background-color: var(--color-violet-700);
        }
    }
}
</style>