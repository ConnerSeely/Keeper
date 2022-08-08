import { AppState } from "../AppState.js"
import { logger } from "../utils/Logger.js"
import { api } from "./AxiosService.js"

class VaultsService {
    async getAll(profileId) {
        const res = await api.get('api/profiles/' + profileId + '/vaults')
        logger.log(res.data)
        AppState.userVaults = res.data
    }
    async getMy(profileId) {
        const res = await api.get('api/profiles/' + profileId + '/vaults')
        logger.log(res.data, 'my vcaults')
        AppState.myVaults = res.data
    }

    async getVaultById(vaultId) {
        const res = await api.get('api/vaults/' + vaultId)
        logger.log(res.data)
        AppState.activeVault = res.data
    }


    async createVault(body) {
        const res = await api.post('api/vaults', body)
        logger.log('created vault', res.data)
        AppState.userVaults.push(res.data)
        AppState.vaults.push(res.data)
        return res.data
    }
    async deleteVault(vaultId) {
        const res = await api.delete('api/vaults/' + vaultId)
        logger.log('deleting vault', res.data)
        AppState.vaults = AppState.vaults.filter(v => v.id != vaultId)
    }
}

export const vaultsService = new VaultsService()