import { defineStore } from "pinia";
import { ref } from "vue";
import { Server } from "../configs/server.ts";
import { useRouter } from "vue-router";

export const useAuth = defineStore("auth", () => {
	const userName = ref<string>('');
	const password = ref<string>('');
	const router = useRouter();

	const login = async () => {
		const response = await fetch(`https://${ Server.HOST }/api/account/login`, {
			method: "POST",
	        headers: {
	            'Accept': 'application/json',
	            'Content-Type': 'application/json'
	        },
	        credentials: "include",
	        body: JSON.stringify({
	        	UserName: userName.value,
	        	Password: password.value
	        })
		});

		const json = await response.json();
		console.log( document.cookie );

		sessionStorage.setItem(Server.NAME_TOKEN, json.token);
		document.cookie = `currentUserId=${ json.currentUserId }`;

		if (json.token) {
			router.push(Server.BASE_URL);
		} 
	};

	return {
		userName,
		password,
		login
	};
});