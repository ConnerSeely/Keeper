<template>
  <div
    data-bs-target="#keep-modal"
    data-bs-toggle="modal"
    class="m-4 elevation-2 d-flex justify-content-between selectable"
    @click="setActive"
  >
    <div class="">
      <img class="img-fluid" :src="keep.img" />
      <div class="d-flex info p-2 text-light">
        <h3 class="">{{ keep.name }}</h3>
        <img
          class="profile-img rounded-pill m-2"
          :src="keep.creator.picture"
          alt=""
          @click.stop="goToProfile"
        />
      </div>
    </div>
  </div>
</template>

<script>
import { keepsService } from '../services/KeepsService.js'
import { logger } from '../utils/Logger.js'
import Pop from '../utils/Pop.js'
import { AppState } from '../AppState'
import { computed } from 'vue'
import { useRouter } from 'vue-router'

export default {
  props: {
    keep: {
      type: Object,
      required: true
    }
  },
  setup(props) {
    const router = useRouter();
    return {
      profile: computed(() => AppState.account),
      async setActive() {
        try {
          keepsService.setActive(props.keep)
        } catch (error) {
          Pop.toast("Something went wrong", "error")
          logger.error(error)
        }
      },
      goToProfile() {
        router.push({ name: "Profile", params: { id: props.keep.creatorId } })
      }
    }
  }
}
</script>


<style lang="scss" scoped>
.profile-img {
  height: 20px;
  width: 20px;
}

.info {
  position: absolute;
  bottom: 1px;
}
</style>