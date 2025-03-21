import { defineStore } from "pinia";
import { ref } from "vue";
import { Server } from "../configs/server.ts";
import { useRouter } from "vue-router";

export const useRegister = defineStore("register", () => {
	const userName = ref<string>('');
	const password = ref<string>('');
	const confirmPassword = ref<string>('');
	const email = ref<string>('');
	const router = useRouter();

	const register = async () => {
		const body = {
			UserName: userName.value,
			Password: password.value,
			ConfirmPassword: confirmPassword.value,
			Email: email.value
		};

		const path: string = '/api/account/register';
		const url: URL = new URL(path, `https://${ Server.HOST }`);

		const response = await fetch(url, {
			method: "POST",
	        headers: {
	            'Accept': 'application/json',
	            'Content-Type': 'application/json'
	        },
	        credentials: "include",
	        body: JSON.stringify(body)
		});

		const json = await response.json();
		console.log( document.cookie );

		sessionStorage.setItem(Server.NAME_TOKEN, json.token);
		document.cookie = `currentUserId=${ json.currentUserId }`;
		router.push(Server.BASE_URL);
	};

	return {
		userName,
		password,
		confirmPassword,
		email,
		register
	};
});