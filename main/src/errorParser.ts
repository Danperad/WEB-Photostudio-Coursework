import errorList from "./assets/errorCodes.json";

export default function errorParser(key: string): string {
  if (key in errorList) {
    // @ts-ignore
    return errorList[key];
  }
  return "Неизвестная ошибка";
}
