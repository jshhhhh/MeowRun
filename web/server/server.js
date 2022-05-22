const express      = require("express")
const cookieParser = require("cookie-parser")
const cors         = require('cors')
const dotenv       = require('dotenv')
const corsOption   = require('./config/CorsOption.js')
const {sequelize}    = require('./models/index.js')


//=================admin-bro========================= //
const AdminJS = require('adminjs')
const AdminJSExpress = require('@adminjs/express')
const options = require('./config/AdminOptions.js')
const auth = require('./middlewares/AdminAuth.js')

const AdminJSSequelize = require('@adminjs/sequelize')
//=================admin-bro========================= //


//We have to tell AdminBro that we will manage mongoose resources with it
AdminJS.registerAdapter(AdminJSSequelize)
const adminJs = new AdminJS(options)
const router = AdminJSExpress.buildAuthenticatedRouter(adminJs,auth)
//... other AdminJSOptions
dotenv.config()
const app = express()
const PORT = process.env.PORT 



sequelize.authenticate()
.then(() => console.log('Connection has been established successfully.'))
.catch(err => console.log('Unable to connect to the database:', err ))




// middleware for cookies
app.use(cookieParser());

// middleware for cors
app.use(cors(corsOption))


app.use(adminJs.options.rootPath, router)



// ====================== routes ====================== // 
// TO DO : 해당 파트 고쳐쓰기
// 랜딩 페이지 루트
app.get('/', (req, res) => {
   res.json('bulid success')
  
})
//dddd
// api 루트
app.get('/apis', (req,res) => {
  
})
// ====================== routes ====================== //


// ====================== middlewares ====================== //

// admin-bro router
 

// built-in middleware for json
app.use(express.json());
// built-in middleware to handle urlencoded data
app.use(express.urlencoded({ extended: false}));
// login 
app.use('/auth', require("./routers/R_Login.js"))
// voting
app.use('/voting', require('./routers/r_voting.js'))
// ====================== middlewares ====================== //



  // Running the server
 

  app.listen(PORT, () => console.log(`Server running on port ${PORT}`))
 

  

   

