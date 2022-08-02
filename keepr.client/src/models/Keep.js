export class Keep {
  constructor(keepData) {
    this.id = keepData.id
    this.creatorId = keepData.creatorId
    this.name = keepData.name
    this.description = keepData.description
    this.img = keepData.img
    this.views = keepData.views
    this.kept = keepData.kept
    this.creator = keepData.creator
  }
}