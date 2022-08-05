<template>
  <form id="vault-form">
    <label>Keep Name</label>
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
    <label>Image</label>
    <input
      v-model="editable.img"
      type="text"
      aria-describedby="helpId"
      required
    />
    <button type="button" data-bs-dismiss="modal">cancel</button>
    <button v-if="!keep" type="button" @click="createKeep">Create!</button>
    <button v-else type="button" @click="editKeep">Save!</button>
  </form>
</template>

<script>
import { ref } from "@vue/reactivity"
import Pop from "../utils/Pop"
import { Modal } from "bootstrap"
import { watchEffect } from '@vue/runtime-core'
import { keepsService } from '../services/KeepsService'
export default {
  props: { keep: { type: Object, required: false } },
  setup(props) {
    const editable = ref({})
    watchEffect(() => {
      editable.value = { ...props.keep }
    })
    return {
      props,
      editable,
      async createKeep() {
        try {
          const keep = await keepsService.createKeep(editable.value)
          Pop.toast('Keep Created!')
          Modal.getOrCreateInstance(document.getElementById('create-keep')).hide()
        } catch (error) {
          Pop.error(error)
        }
      },
      async editKeep() {
        try {
          await keepsService.editKeep(editable.value)
          Pop.toast('Keep updated!')
          Modal.getOrCreateInstance(document.getElementById('edit-keep')).hide()
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