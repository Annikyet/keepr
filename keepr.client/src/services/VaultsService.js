import { AppState } from '../AppState'
import { logger } from '../utils/Logger'
import { api } from './AxiosService'

class VaultsService {
  async getVault(vaultId) {
    try {
      const res = await api.get('api/vaults/' + vaultId)
      AppState.activeVault = res.data
    } catch (error) {
      logger.error(error)
    }
  }

  async getVaultKeeps(vaultId) {
    try {
      const res = await api.get('api/vaults/' + vaultId + '/keeps')
      AppState.vaultKeeps = res.data
    } catch (error) {
      logger.error(error)
    }
  }

  async createVault(vaultData) {
    try {
      const res = await api.post('api/vaults', vaultData)
      // logger.log(res.data)
      AppState.profileVaults.push(res.data)
    } catch (error) {
      logger.error(error)
    }
  }
}

export const vaultsService = new VaultsService()