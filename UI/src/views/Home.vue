<template>
    <div class="chat">
        <Sidebar :messages="messages"/>

        <div class="chat-messages">
            <div class="chat-messages__message">
                <Message 
                    v-for="message in messages?.Messages" 
                    :key="message.Id"  
                    :message="message"
                    :members="messages?.Members"
                />
            </div>

            <div class="chat-messages__input">
                <div class="input-wrap">
                    <ClipIcon />
                    <input
                        type="text" 
                        name="message" 
                        class="messages-input" 
                        placeholder="Написать сообщение..."
                        maxlength="512"
                        v-model="message"
                    >
                    <SmileIcon />
                    <button class="btn" @click="sendMessage">
                        <SendIcon />
                    </button>
                </div>
            </div>
        </div>
    </div>
</template>

<script setup lang="ts">
import ClipIcon from "../assets/icons/clip.svg";
import SmileIcon from "../assets/icons/smile.svg";
import SendIcon from "../assets/icons/send.svg";
import Message from "../components/Message.vue";
import Header from "../components/Header.vue";
import Sidebar from "../components/Sidebar.vue";
import { Server } from "../configs/server.ts";
import { store } from "../stores/useCurrentUser.ts";
import { MessageModel } from "../components/Models/MessageModel.ts";
import { ref, onMounted } from "vue";

const message = ref<string>(null);
const messages = ref<MessageModel>(null);

const JWT_TOKEN = sessionStorage.getItem(Server.NAME_TOKEN);
const currentUserId = document.cookie.split('=')[1];

const getMessages = async () => {
    const response = await fetch(`https://${ Server.HOST }/chat/1`, {
        method: "GET",
        headers: {
            'Authorization': `Bearer ${ JWT_TOKEN }`
        },
        credentials: "include"
    });

    messages.value = await response.json();
};

const sendMessage = async () => {
    if (message.value === null || message.value === '') {
        return;
    }

    console.log(currentUserId);

    const body: MessageModel = {
        Text: message.value,
        CreatedDateTime: new Date().toISOString(),
        ChatId: 1,
        UserId: Number(currentUserId)
    };

    const response = await fetch(`https://${ Server.HOST }/chat/send`, {
        method: "POST",
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json',
            'Authorization': `Bearer ${ JWT_TOKEN }`
        },
        credentials: "include",
        body: JSON.stringify(body)
    });

    messages.value?.Messages.push(body);
    message.value = '';
};

onMounted(() => {
    getMessages();
});
</script>

<style scoped>
    .chat {
        width: 100%;
        height: 100vh;
    }

    .chat-messages {
        width: calc(100% - 400px);
        height: 100%;
        margin-left: 400px;

        display: flex;
        flex-direction: column;
        justify-content: space-between;

        .header {
            padding: 8px;
            color: #fff;
            background-color: rgba(123, 123, 123, 0.3);

            .header-radio {
                display: flex;
                gap: 24px;

                .header-radio__item {
                    display: flex;
                    align-items: center;
                    gap: 8px;
                }
            }
        }

        .chat-messages__message {
            padding: 24px;
            overflow-y: auto;

            background-color: #f7f6fa;
        }

        .chat-messages__input {
            width: 100%;
            padding: 24px;

            display: flex;
            align-items: center;
            gap: 16px;

            background-color: #f7f6fa;

            .input-wrap {
                width: 100%;
                padding: 8px 12px;
                border-radius: 12px;

                display: flex;
                align-items: center;
                gap: 16px;

                background-color: #fff;
            }
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
