import errorList from "./assets/errorCodes.json";

export default function errorParser (key: string) : string{
    if (key in errorList) {
        // eslint-disable-next-line @typescript-eslint/ban-ts-comment
        // @ts-expect-error
        return errorList[key];
    }
    return "Неизвестная ошибка";
}