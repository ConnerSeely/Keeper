<template>
  <form id="vault-form">
    <label>Vault Name</label>
    <input
      v-model="editable.name"
      type="text"
      aria-describedby="helpId"
      required
    />
    <label>Description</label>
    <input
      v-model="editable.description"
      type="text"
      aria-describedby="helpId"
      required
    />
    <label>Private</label>
    <input
      v-model="editable.isPrivate"
      type="checkbox"
      aria-describedby="helpId"
      required
    />
    <button type="button" data-bs-dismiss="modal">cancel</button>
    <button v-if="!vault" type="button" @click="createVault">Create!</button>
    <button v-else type="button" @click="editVault">Save!</button>
  </form>
</template>

<script>
import { ref } from "@vue/reactivity"
import Pop from "../utils/Pop"
import { Modal } from "bootstrap"
import { watchEffect } from '@vue/runtime-core'
import { vaultsService } from '../services/VaultsService'
import { useRouter } from 'vue-router'
export default {
  props: { vault: { type: Object, required: false } },
  setup(props) {
    const editable = ref({})
    const router = useRouter()
    watchEffect(() => {
      editable.value = { ...props.vault }
    })
    return {
      props,
      editable,
      async createVault() {
        try {
          const vault = await vaultsService.createVault(editable.value)
          Pop.toast('Vault Created!')
          Modal.getOrCreateInstance(document.getElementById('create-vault')).hide()
        } catch (error) {
          Pop.error(error)
        }
      },
      async editVault() {
        try {
          await vaultsService.editVault(editable.value)
          Pop.toast('Vault updated!')
          Modal.getOrCreateInstance(document.getElementById('edit-vault')).hide()
        } catch (error) {
          Pop.error(error)
        }
      }
    }
  }
}
</script>

<style lang="scss" scoped>
@import "../assets/scss/_vaultForm.scss";
</style>