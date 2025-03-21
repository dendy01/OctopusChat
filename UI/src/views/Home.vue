<template>
    <div class="chat">
        <Sidebar :messages="messages"/>

        <div class="chat-messages">
            <Header />

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
                    <ClipIcon class="input-wrap__clip"/>
                    <input
                        type="text" 
                        name="message" 
                        class="messages-input" 
                        placeholder="Написать сообщение..."
                        maxlength="512"
                        v-model="message"
                    >
                    <SmileIcon class="input-wrap__smile"/>
                    <button class="btn" @click="sendMessage">
                        <SendIcon class="input-wrap__send"/>
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
import { useRouter } from "vue-router";

const message = ref<string>(null);
const messages = ref<MessageModel>(null);

const JWT_TOKEN = sessionStorage.getItem(Server.NAME_TOKEN);
const currentUserId = Number(document.cookie.split('=')[1]);

const router = useRouter();

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
        UserId: currentUserId
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

const checkAuth = () => {
    if (!JWT_TOKEN) {
        router.push(`authorize`);
    }
};

onMounted(() => {
    checkAuth();
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

        background-color: var(--color-white-400);

        .chat-messages__message {
            padding: var(--size-xl);
            overflow-y: auto;

            background-color: var(--color-white-400);
        }

        .chat-messages__message::-webkit-scrollbar {
            width: 5px;

            background-color: transparent;
        }

        .chat-messages__message::-webkit-scrollbar-thumb {
            width: 5px;
            border-radius: var(--size-md);

            background-color: transparent;
        }

        .chat-messages__message:hover::-webkit-scrollbar-thumb {
            width: 5px;
            border-radius: var(--size-md);

            background-color: rgba(46, 38, 61, .4);
        }

        .chat-messages__input {
            width: 100%;
            padding: var(--size-xl);

            display: flex;
            align-items: center;
            gap: var(--size-lg);

            background-color: var(--color-white-400);

            .input-wrap {
                width: 100%;
                padding: var(--size-sm) var(--size-md);
                border-radius: var(--size-md);

                display: flex;
                align-items: center;
                gap: var(--size-lg);

                background-color: var(--color-white-700);

                .input-wrap__smile,
                .input-wrap__send,
                .input-wrap__clip {
                    cursor: pointer;
                    min-width: var(--size-xl);
                }

                .input-wrap__smile:active,
                .input-wrap__send:active,
                .input-wrap__clip:active {
                    transform: scale(.9);
                }
            }
        }

        .messages-input {
            height: 30px;
            width: 100%;
            font-size: var(--fs-lg);
            background: transparent;
        }

        .messages-input:-webkit-autofill {
            -webkit-box-shadow: 0 0 0px 1000px var(--color-white-700) inset;
        }
    }

    .btn {
        background: none;
    }
</style>
