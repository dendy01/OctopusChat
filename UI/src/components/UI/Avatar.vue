<template>
	<div class="avatar">
		<div v-if="img" class="avatar-img">
			<img :src="img">
		</div>

		<div v-else class="avatar-noicon">
			{{ text?.split('')[0] }}
		</div>
	</div>
</template>

<script setup lang="ts">
interface IPropsType {
	img?: string;
	text?: string;
}

defineProps<IPropsType>();
</script>

<style scoped>
.avatar {
	width: 40px;
    height: 40px;
    min-width: 40px;
    min-height: 40px;
    border-radius: 50%;
    overflow: hidden;

    user-select: none;
}

.avatar-img, img {
	object-fit: cover;
}

.avatar-img,
.avatar-noicon {
	width: 100%;
	height: 100%;
}

.avatar-noicon {
	font-size: var(--fs-xl);
	font-weight: bold;

	display: flex;
    align-items: center;
    justify-content: center;

    color: var(--color-green-700);
    background-color: var(--color-green-400);
}
</style>