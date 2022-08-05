import { AppState } from '../AppState'
import { logger } from '../utils/Logger'
import { api } from './AxiosService'
import Pop from '../utils/Pop'

class KeepsService {
  async getAll() {
      const res = await api.get('api/keeps')
      AppState.keeps = res.data
  }

  async getById(id) {
      const res = await api.get('api/keeps/' + id)
      AppState.activeKeep = res.data
  }

  async create(newKeep) {
      const res = await api.post('api/keeps', newKeep)
      // logger.log(res.data)
      AppState.profileKeeps.push(res.data)
      AppState.keeps.push(res.data)
      // this.getAll()
  }

  async delete(keepId) {
      const res = await api.delete('api/keeps/' + keepId)
      // logger.log(res.data)
      // AppState.keeps.filter((k) => k.id != res.data)
      // AppState.keeps.filter((k) => k.id != keepId)
      this.getAll()
  }
}

export const keepsService = new KeepsService()