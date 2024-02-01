import crypto from "crypto";

export default function generateHash(value: any) {
    const instant = crypto.createHash("sha256")
    return instant.update(value).digest().toString()
}