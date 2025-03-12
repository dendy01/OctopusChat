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
	width: 60px;
    height: 60px;
    min-width: 60px;
    min-height: 60px;
    border-radius: 50%;
    overflow: hidden;
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
	font-size: 18px;
	font-weight: bold;

	display: flex;
    align-items: center;
    justify-content: center;

    color: #56ca00;
    background-color: #e4f7d6;
}
</style>