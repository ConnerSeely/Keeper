<template>
  <Modal id="keep-modal">
    <template #modal-body>
      <div class="container">
        <div class="row">
          <div class="col-md-6">
            <img class="img-fluid keep-img" :src="keep.img" />
          </div>
          <div class="col-md-6">
            <div class="ms-4 d-flex flex-column justify-content-center">
              <i class="mdi mdi-account-eye text-center"
                >{{ keep.views }}
                <i class="mdi mdi-safe-square-outline text-center">{{
                  keep.kept
                }}</i></i
              >

              <h1 class="pb-2 text-center">{{ keep.name }} <br /></h1>
              <h6 class="pb-4 border-bottom border-secondary">
                {{ keep.description }}
                Lorem ipsum dolor sit, amet consectetur adipisicing elit. At
                aspernatur animi incidunt neque maiores expedita quisquam earum
                laudantium, nobis nostrum eos tempora architecto, laboriosam
                veniam laborum nihil, velit ratione ullam? Magni, nihil
                blanditiis earum amet accusantium facere consectetur, quibusdam
                necessitatibus laborum aperiam laboriosam enim sapiente ab ipsum
                sit? Ab deserunt officiis aliquam ullam explicabo sapiente ad
                repellat cupiditate. Iste, accusantium.
              </h6>
              <div>
                <i class="mdi mdi-delete text-danger" @click="deleteKeep"></i>
                <!-- TODO ADD CREATOR PICTURE AND ADD TO VAULT DROPDOWN -->
                <img class="profile-img rounded-pill" :src="keep" alt="" />
              </div>
            </div>
          </div>
        </div>
      </div>
    </template>
  </Modal>
</template>

<script>
import { computed } from 'vue'
import { AppState } from '../AppState.js'
import { keepsService } from '../services/KeepsService.js'
import { logger } from '../utils/Logger.js'
import Pop from '../utils/Pop.js'

export default {
  setup() {
    return {
      keep: computed(() => AppState.activeKeep),
      // TODO ADD KEEP TO VAULT
      // async addKeep() {
      //   try {
      //     await keepsService.addKeep({ body: review.body, rating: review.rating, restaurantId: AppState.activeRestaurant.id })
      //   } catch (error) {
      //     Pop.toast("hmm wrong", 'error')
      //     logger.log(error)
      //   }
      // },
      //TODO FIX DELETE IT DOESN'T WORK
      async deleteKeep() {
        try {
          await keepsService.deleteKeep()
        } catch (error) {
          Pop.toast("hmm wrong", 'error')
          logger.log(error)
        }
      }
    }
  }
}
</script>
<style scoped>
.keep-img {
  width: 100%;
  object-fit: cover;
}
.profile-img {
  width: 35px;
}
</style>