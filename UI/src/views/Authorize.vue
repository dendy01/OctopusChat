<template>
	<div class="authorize">
		<div class="authorize-btns">
			<button :class="['btn', isSignIn && 'btn-active']" @click="isSignIn = !isSignIn">Войти</button>
			<button :class="['btn', !isSignIn && 'btn-active']" @click="isSignIn = !isSignIn">Регистрация</button>
		</div>

		<div class="authorize-forms">
			<div :class="['authorize-forms__signin', isSignIn && 'signin-active']">
				<form v-if="isSignIn">
					<label for="username">Имя</label>
					<input
						type="text" 
						id="username" 
						name="username" 
						maxlength="64"
						v-model="storeAuth.userName"
						required
					>

					<label for="password">Пароль</label>
					<input 
						type="password" 
						id="password" 
						name="password"
						minlength="8" 
						maxlength="64"
						v-model="storeAuth.password"
						required
					>

					<button type="submit" class="btn" @click.prevent="storeAuth.login">Войти</button>
				</form>
			</div>

			<div :class="['authorize-forms__signup', !isSignIn && 'signup-active']">
				<form v-if="!isSignIn">
					<label for="username">Имя</label>
					<input
						type="text" 
						id="username" 
						name="username" 
						maxlength="64"
						v-model="storeRegister.userName"
						required
					>

					<label for="password">Пароль</label>
					<input 
						type="password" 
						id="password" 
						name="password"
						minlength="8" 
						maxlength="64"
						v-model="storeRegister.password"
						required
					>

					<label for="repeat-password">Повторите пароль</label>
					<input
						type="password"
						id="repeat-password"
						name="password"
						minlength="8" 
						maxlength="64"
						v-model="storeRegister.confirmPassword"
						required
					>

					<label for="phone">Телефон</label>
					<input 
						type="phone" 
						id="phone" 
						name="phone" 
						maxlength="64"
						required
					>

					<label for="email">E-mail</label>
					<input 
						type="email" 
						id="email" 
						name="email" 
						maxlength="64"
						v-model="storeRegister.email"
						required
					>

					<button type="submit" class="btn" @click.prevent="storeRegister.register">Регистрация</button>
				</form>
			</div>
		</div>
	</div>
</template>

<script setup lang="ts">
import { ref } from "vue";
import { useAuth } from "../store/auth.ts";
import { useRegister } from "../store/register.ts";

const isSignIn = ref(true);

const storeAuth = useAuth();
const storeRegister = useRegister();
</script>

<style scoped>
.authorize {
	width: 400px;
	margin: 50px auto;
	padding: 20px;
	border-radius: 8px;

	color: #fff;
	background-image: linear-gradient(135deg, #000, #09f 75%, #fff);

	.authorize-btns {
		display: flex;
		gap: 16px;

		.btn {
			padding-bottom: 4px;
			font-size: 18px;
			cursor: pointer;

			border-bottom: 3px solid transparent;

			opacity: .6;
			color: #fff;
			background: none;
		}

		.btn-active {
			opacity: 1;
			border-bottom: 3px solid #2973B2;
		}
	}

	.authorize-forms {
		margin: 20px 0;
		overflow: hidden;
		position: relative;

		display: flex;

		input {
			padding: 8px 16px;
			margin: 4px 0 16px 0;
			font-size: 18px;
			border-radius: 16px;

			background-color: rgba(255, 255, 255, .3);
			color: #fff;
		}

		.btn {
			width: 100%;
			padding: 8px 16px;
			margin-top: 16px;
			font-size: 18px;
			font-weight: bold;

			border-radius: 16px;

			background-color: #6c757d;
			color: #fff;
		}

		.authorize-forms__signin {
			min-width: 100%;

			position: relative;
			transform: translateX(-100%);
			transition: transform .3s ease;
		}

		.signin-active {
			transform: translateX(0);
		}

		.authorize-forms__signup {
			min-width: 100%;
			position: relative;
			transform: translateX(100%);
			transition: transform .3s ease;
		}

		.signup-active {
			transform: translateX(-100%);
		}
	}
}
</style>