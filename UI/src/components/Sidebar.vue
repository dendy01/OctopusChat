<template>
	<aside class="chats">
        <div class="chats-avatar">
            <Avatar :text="messages?.Members[currentUserId]"/>

            <label for="search" class="chats-avatar__search">
                <Search />
                <input type="search" name="search" id="search" placeholder="Search...">
            </label>
        </div>

        <p class="chats-title">Чаты</p>

		<ul class="chats-users">
            <li class="chats-users__user chats-active">
                <Avatar :img="AvatarIcon" :text="getUserChats()"/>

                <div class="user-name">
                    <h4 class="name">{{ getUserChats() }}</h4>
                    <p class="last-message">{{ getLastMessage() }}</p>
                </div>
            </li>
        </ul>
	</aside>
</template>

<script setup lang="ts">
import AvatarIcon from "../assets/user_icon.jfif";
import Search from "../assets/icons/search.svg";
import MessageModel from "./Models/MessageModel.ts";
import Avatar from "./UI/Avatar.vue";

interface IPropsType {
	messages: MessageModel;
}

const props = defineProps<IPropsType>();
const currentUserId = document.cookie.split('=')[1];

const getUserChats = (messages) => {
    return props.messages?.Members[currentUserId === "4" ? "3" : "4"];
};

const getLastMessage = () => {
    return props.messages?.Messages.at(-1).Text
};
</script>

<style setup>
.chats {
    width: 400px;
    height: 100%;

    position: fixed;
    top: 0;
    left: 0;

    border-right: 1px solid var(--color-gray-700);
    background-color: var(--color-white-700);

    .chats-avatar {
        height: 75px;
        padding: 20px;
        border-bottom: 1px solid var(--color-gray-700);

        display: flex;
        align-items: center;
        gap: 20px;

        .chats-avatar__search {
            width: 100%;
            padding: var(--size-sm);
            cursor: text;

            border-radius: 50px;
            border: 2px solid var(--color-gray-700);

            display: flex;
            align-items: center;
            gap: var(--size-sm);

            input {
                pointer-events: none;
            }
        }

        .chats-avatar__search:focus-within {
            border: 2px solid var(--color-violet-700);
        }
    }

    .chats-title {
        padding: 20px;
        font-size: var(--fs-xl);
        font-weight: bold;

        color: var(--color-violet-700);
    }

    .chats-users {
        padding: 0 var(--size-md);

        .chats-users__user {
            padding: var(--size-sm);
            border-radius: var(--size-sm);

            display: flex;
            align-items: center;
            gap: 20px;

            .name {
                font-size: var(--fs-md);
                font-weight: 500;
            }
        }
    }

    .chats-users__user:hover {
        background-color: var(--color-green-500);
    }

    .last-message {
        margin-top: 4px;
        font-size: var(--fs-sm);
        word-break: break-word;
    }

    .chats-active {
        color: var(--color-white-700);
        background-color: var(--color-violet-700);
    }

    .chats-active:hover {
        background-color: var(--color-violet-700);
    }
}
</style>