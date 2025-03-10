import { createRouter, createWebHistory } from 'vue-router';
import { Server } from "../configs/server.ts";

const routes = [
	{
		path: `${ Server.BASE_URL }`,
		component: () => import ("../views/Home.vue")
	},
	{
		path: `${ Server.BASE_URL }/authorize`,
		component: () => import ("../views/Authorize.vue")
	}
];

const router = createRouter({
	history: createWebHistory(),
	routes
});

export default router;