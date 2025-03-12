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
                <Avatar :img="AvatarIcon" :text="messages?.Members[currentUserId === '4' ? '3' : '4']"/>

                <div class="user-name">
                    <h4 class="name">{{ messages?.Members[currentUserId === "4" ? "3" : "4"] }}</h4>
                    <p class="last-message">{{ messages?.Messages.at(-1).Text }}</p>
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

defineProps<IPropsType>();
const currentUserId = document.cookie.split('=')[1];
</script>

<style setup>
.chats {
    width: 400px;
    height: 100%;

    position: fixed;
    top: 0;
    left: 0;

    border-right: 1px solid #e6e5e8;
    background-color: white;

    .chats-avatar {
        height: 95px;
        padding: 20px;
        border-bottom: 1px solid #e6e5e8;

        display: flex;
        align-items: center;
        gap: 20px;

        .chats-avatar__search {
            width: 100%;
            padding: 8px;

            border-radius: 50px;
            border: 2px solid #e6e5e8;

            display: flex;
            align-items: center;
            gap: 8px;
        }
    }

    .chats-title {
        padding: 20px;
        font-size: 18px;
        font-weight: bold;

        color: #8c57ff;
    }

    .chats-users {
        padding: 0 12px;

        .chats-users__user {
            padding: 8px;
            border-radius: 8px;

            display: flex;
            align-items: center;
            gap: 20px;
        }
    }

    .chats-users:hover {
        background-color: #D5E5D5;
    }

    .last-message {
        margin-top: 4px;
        word-break: break-word;
    }

    .chats-active {
        color: #fff;
        background-color: #8c57ff;
    }

    .chats-active:hover {
        background-color: #8c57ff;
    }
}
</style>