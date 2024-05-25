import {useRegisterSW} from "virtual:pwa-register/react";
import {useRef} from "react";
import {HttpTransportType, HubConnection, HubConnectionBuilder} from "@microsoft/signalr";

const baseUrl = import.meta.env.VITE_API_URL;

function ServiceWorker() {
  const connection = useRef<HubConnection | null>(null)
  const isAllowNotification = useRef<boolean>(false)
  useRegisterSW({
    onRegisteredSW(_, registration) {
      connection.current = new HubConnectionBuilder().withUrl(`${baseUrl ? baseUrl : `/api`}/hub`, {
        skipNegotiation: true,
        transport: HttpTransportType.WebSockets
      }).build()
      if (Notification.permission === "default") {
        Notification.requestPermission().then(res => isAllowNotification.current = res === "granted")
      } else {
        isAllowNotification.current = Notification.permission === "granted"
      }
      connection.current?.on("NewClient", () => {
        console.log("NewClient")
        if (isAllowNotification.current) {
          registration!.showNotification("Новый клиент", {
            body: "В базе новый клиент",
            tag: "new-client",
          })
        }
      })
      connection.current?.on("NewOrder", () => {
        if (isAllowNotification.current) {
          registration!.showNotification("Новая заявка", {
            body: "В базе новая заявка",
            tag: "new-order",
          })
        }
      })
      connection.current?.on("StatusChanged", data => {
        if (isAllowNotification.current) {
          registration!.showNotification("Новый статус заявки", {
            body: `У заявки ${data} изменился статус`,
            tag: "order-status-changed",
          })
        }
      })
      connection.current?.start().catch(console.error)
    },
    onRegisterError(error) {
      console.error('SW registration error', error)
    },
  })
  return (
    <></>
  )
}

export {ServiceWorker}
