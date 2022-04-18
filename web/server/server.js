const express      = require("express")
const cookieParser = require("cookie-parser")
const cors         = require('cors')
const dotenv       = require('dotenv')
const mongoose     = require('mongoose')
const corsOption   = require('./config/CorsOption.js')


// =================admin-bro========================= //
const AdminBro = require('admin-bro')
const AdminBroMongoose = require('admin-bro-mongoose')
const options = require('./config/AdminOptions.js')
const AdminBroExpress = require('admin-bro-expressjs')
const auth = require('./middlewares/AdminAuth.js')
// =================admin-bro========================= //


// We have to tell AdminBro that we will manage mongoose resources with it
AdminBro.registerAdapter(AdminBroMongoose)
const adminBro = new AdminBro(options)
const router = AdminBroExpress.buildAuthenticatedRouter(adminBro,auth)

dotenv.config()
const app = express()
const PORT = process.env.PORT 


// middleware for cookies
app.use(cookieParser());

// middleware for cors
app.use(cors(corsOption))





// ====================== routes ====================== // 
// TO DO : 해당 파트 고쳐쓰기
// 랜딩 페이지 루트
app.get('/', (req, res) => {
  
  
})

// api 루트
app.get('/apis', (req,res) => {
  
})
// ====================== routes ====================== //


// ====================== middlewares ====================== //

// admin-bro router
app.use(adminBro.options.rootPath, router)

// built-in middleware for json
app.use(express.json());
// built-in middleware to handle urlencoded data
app.use(express.urlencoded({ extended: false}));
// login 
app.use('/auth', require("./routers/R_Login.js"))
// ====================== middlewares ====================== //



  // Running the server
  const run = async () => {
      await mongoose.connect(process.env.MONGO_URI)
      app.listen(PORT, () => console.log(`Server running on port ${PORT}`))
    }

  run()

   

