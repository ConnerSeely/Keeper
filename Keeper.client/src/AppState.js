import { reactive } from 'vue'

// NOTE AppState is a reactive object to contain app level data
export const AppState = reactive({
  user: {},
  account: {},
  keeps: [],
  activeKeep: {},
  activeVault: {},
  activeProfile: {},
  vaults: [],
  userVaults: [],
  myVaults: [],
  userKeeps: [],
})
