import { signIn } from "next-auth/react"
import { pages } from "next/dist/build/templates/app-page"

export { default } from "next-auth/middleware"

export const config = {
     matcher: [
        "/session"
    ],
    pages: {
        signIn: '/api/auth/signin'
    }
 }