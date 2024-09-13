// export { GET, POST } from "@/auth";


import NextAuth, { NextAuthOptions } from "next-auth"
import DuendeIdentityServer6 from "next-auth/providers/duende-identity-server6"

export const authOptions: NextAuthOptions = {
            session: {
                strategy: 'jwt'
            },
            providers: [
                    DuendeIdentityServer6({
                        id: 'id-server',
                        clientId: 'nextApp',
                        clientSecret: 'secret',
                        issuer: 'http://localhost:5000',
                        authorization: { params: { scope: 'openid profile auctionApp' }
                        },
                        idToken: true
                    })

                    // C:\Users\ZBOOK 14U G6\Desktop\DecagonClass\Carluxe\frontend\web-app\.ts
                // ...add more providers here
            ],

            callbacks: {
                jwt({token, profile, account}) {
                    if(profile){
                        token.username = profile.username
                    }

                    if (account) {
                        token.access_token = account.access_token
                    }
                    return token;
                },

                async session({session, token}) {
                    if(token) {
                        session.user.username = token.username
                    }

                    return session;
                }
            }
}

 const handlers = NextAuth(authOptions);
 export {handlers as GET, handlers as POST}



  
//                 DuendeIdentityServer6({
//                     id: 'id-server',
//                     clientId: 'nextApp',
//                     clientSecret: 'secret',
//                     issuer: 'http://localhost:5000',
//                     authorization: {params: {scope: 'openid profile auctionApp'}},
//                     idToken: true
//                 })




 

