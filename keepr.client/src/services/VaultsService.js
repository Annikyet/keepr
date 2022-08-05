import { AppState } from '../AppState'
import { logger } from '../utils/Logger'
import { api } from './AxiosService'
import Pop from '../utils/Pop'
import { profilesService } from './ProfilesService'
import { vaultKeepsService } from './VaultKeepsService'
// import { useRouter, useRoute } from 'vue-router'
class VaultsService {
  async getVault(vaultId) {
      logger.log('gv')
      logger.log(vaultId)
      const res = await api.get('api/vaults/' + vaultId)
      AppState.activeVault = res.data
  }
  
  async getVaultKeeps(vaultId) {
      logger.log('gvk')
      logger.log(vaultId)
      const res = await api.get('api/vaults/' + vaultId + '/keeps')
      AppState.vaultKeeps = res.data
  }

  async createVault(vaultData) {
      const res = await api.post('api/vaults', vaultData)
      // logger.log(res.data)
      // AppState.myVaults.push(res.data)
      profilesService.getVaults(AppState.account.id)
      AppState.myVaults = AppState.profileVaults
  }

  async delete(vaultId, vaultKeeps) {
      await api.delete('api/vaults/' + vaultId)
      profilesService.getVaults(AppState.account.id)
      AppState.myVaults = AppState.profileVaults
  }
}

export const vaultsService = new VaultsService()