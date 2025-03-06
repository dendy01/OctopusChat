<template>
    <div class="chat">
        <div class="chat-contacts">
            <div class="chat-contacts__user contacts-active">
                <div class="user-icon">
                    <img src="./assets/user_icon.jfif">
                </div>
                <div class="user-name">
                    <h3 class="name">User name</h3>
                    <p class="last-message">Hello friend</p>
                </div>
            </div>

            <div class="chat-contacts__user">
                <div class="user-icon">
                    <img src="./assets/user_icon.jfif">
                </div>
                <div class="user-name">
                    <h3 class="name">User name</h3>
                    <p class="last-message">Hello friend</p>
                </div>
            </div>
        </div>

        <div class="chat-messages">
            <div class="chat-messages__message">
                <div class="message-user">
                    <h3 class="message-user__name">You message</h3>
                    <p class="message-user__text">Hello OctopusChat</p>
                    <p>time: 12:23</p>
                </div>

                <div class="message-user user-friend">
                    <h3 class="message-user__name">Friend message</h3>
                    <p class="message-user__text">Hello you</p>
                    <p>time: 12:23</p>
                </div>
            </div>

            <div class="chat-messages__input">
                <ClipIcon />
                <input type="text" name="message" class="messages-input" placeholder="Написать сообщение...">
                <SmileIcon />
                <button class="btn">
                    <SendIcon />
                </button>
            </div>
        </div>
    </div>
</template>

<script setup lang="ts">
import ClipIcon from "./assets/icons/clip.svg";
import SmileIcon from "./assets/icons/smile.svg";
import SendIcon from "./assets/icons/send.svg";
import { onMounted } from "vue";

onMounted(async () => {
    const response = await fetch("https://localhost/chat/1");
    const messages = await response.json();

    console.log(messages);
});
</script>

<style scoped>
    .chat {
        width: 100%;
        height: 100vh;
        background-color: #80CBC4;
    }

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
            background-color: #4D55CC;
        }

        .contacts-active:hover {
            background-color: #4D55CC;
        }
    }

    .chat-messages {
        width: calc(100% - 400px);
        height: 100%;
        margin-left: 400px;

        display: flex;
        flex-direction: column;
        justify-content: space-between;

        .chat-messages__message {
            padding: 12px;

            .message-user {
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

            .message-user:first-child {
                margin-top: 0;
            }

            .user-friend {
                float: right;
                text-align: right;
            }
        }

        .chat-messages__input {
            width: 100%;
            padding: 8px 12px;

            display: flex;
            align-items: center;
            gap: 16px;

            background-color: #FBF8EF;
        }

        .messages-input {
            height: 40px;
            width: 100%;
            font-size: 16px;
            background: none;
        }
    }

    .btn {
        background: none;
    }
</style>
