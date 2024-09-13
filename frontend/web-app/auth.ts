// import NextAuth, { NextAuthOptions } from "next-auth"
// import DuendeIdentityServer6 from "next-auth/providers/duende-identity-server6"

// export const authOptions: NextAuthOptions = {
//   // Configure one or more authentication providers
//         session: {
//             strategy: 'jwt'
//         },
//   providers: [
//     DuendeIdentityServer6({
//         id: 'id-server',
//       clientId: 'nextApp',
//       clientSecret: 'secret',
//       issuer: 'http://localhost:5000',
//       authorization: {params: {scope: 'openid profile auctionApp'}},
//       idToken: true
//     }),
//     // ...add more providers here
//   ],
// }

//  const handlers = NextAuth(authOptions);
//  export {handlers as GET, handlers as POST}













// import NextAuth from "next-auth"
// import DuendeIdentityServer6 from "next-auth/providers/duende-identity-server6";
 
// export const { 
//     handlers: { GET, POST}, auth, signIn, signOut } = NextAuth({
//     session: {
//                     strategy: 'jwt'
//                 },
//   providers: [
//             DuendeIdentityServer6 ({

//                 id: 'id-server',
//                 clientId: 'nextApp',
//                 clientSecret: 'secret',
//                 issuer: 'http://localhost:5000',
//                 authorization: {params: {scope: 'openid profile auctionApp'}},
                
//   })],
// })