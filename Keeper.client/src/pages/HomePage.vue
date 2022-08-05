<template>
  <div class="masonry-frame">
    <div v-for="k in keeps" :key="k.id">
      <Keep :keep="k" />
    </div>
  </div>
</template>

<script>
import { computed, onMounted } from 'vue'
import { AppState } from '../AppState.js'
import { keepsService } from '../services/KeepsService.js'
import { logger } from '../utils/Logger.js'
import Pop from '../utils/Pop.js'

export default {
  name: 'Home',
  setup() {
    onMounted(async () => {
      try {
        await keepsService.getAll()
      } catch (error) {
        Pop.toast('Failed to load Keeps', 'error')
        logger.error(error)
      }
    })
    return {
      keeps: computed(() => AppState.keeps)
    }
  }
}
</script>

<style scoped lang="scss">
.masonry-frame {
  columns: 4;

  div {
    break-inside: avoid;
  }
}
</style>
