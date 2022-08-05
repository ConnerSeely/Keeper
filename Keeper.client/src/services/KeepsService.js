import { AppState } from "../AppState.js"
import { api } from "./AxiosService.js"
import { logger } from '../utils/Logger.js'

class KeepsService {
  async getAll() {
    const res = await api.get('api/keeps')
    AppState.keeps = res.data
  }
  async getProfileKeeps(profileId) {
    const res = await api.get('api/profiles/' + profileId + '/keeps')
    AppState.userKeeps = res.data
  }

  async setActive(keep) {
    AppState.activeKeep = keep
    const res = await api.get(`api/keeps/${keep.id}`)
  }

  async createKeep(body) {
    const res = await api.post('api/keeps', body)
    logger.log('created keep', res.data)
    AppState.userKeeps.push(res.data)
    AppState.keeps.push(res.data)
    return res.data
  }
  async deleteKeep(keepId) {
    const res = await api.delete('api/keeps/' + keepId)
    logger.log('deleting keep', res.data)
    AppState.keeps = AppState.keeps.filter(k => k.id != keepId)
  }
}

export const keepsService = new KeepsService()