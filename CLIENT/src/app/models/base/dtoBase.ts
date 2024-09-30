import {uuid} from "uuidv4";

export interface DtoBase
{
  id: string,
  folderUpload: string,
  isActived: boolean | true,
  isEdit: boolean | false,
  sort: number | 0,
}

export function createDefaultDtoBase(): DtoBase {
  return {
    id: uuid(),
    folderUpload: uuid(),
    isActived: true,
    isEdit: false,
    sort: 0,
  }
}
