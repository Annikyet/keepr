import { AppState } from '../AppState'
import { logger } from '../utils/Logger'
import { api } from './AxiosService'
import Pop from '../utils/Pop'

class KeepsService {
  async getAll() {
    try {
      const res = await api.get('api/keeps')
      AppState.keeps = res.data
    } catch (error) {
      logger.error(error)
      Pop.error(error)
    }
  }

  async getById(id) {
    try {
      const res = await api.get('api/keeps/' + id)
      AppState.keeps = res.data
    } catch (error) {
      logger.error(error)
      Pop.error(error)
    }
  }

  async create(newKeep) {
    try {
      const res = await api.post('api/keeps', newKeep)
      // logger.log(res.data)
      AppState.profileKeeps.push(res.data)
      AppState.keeps.push(res.data)
      // this.getAll()
    } catch (error) {
      logger.error(error)
      Pop.error(error)
    }
  }

  async delete(keepId) {
    try {
      const res = await api.delete('api/keeps/' + keepId)
      // logger.log(res.data)
      // AppState.keeps.filter((k) => k.id != res.data)
      // AppState.keeps.filter((k) => k.id != keepId)
      this.getAll()
    } catch (error) {
      logger.error(error)
      Pop.error(error)
    }
  }
}

export const keepsService = new KeepsService()