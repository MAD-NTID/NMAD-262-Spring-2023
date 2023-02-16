const express = require('express')
const app = express()
const port = 5000

//force our app to accept json for the body
app.use(express.json());


let movies = [
    {"title":"Star War", "id":100, "rank":1, "rating":10.0},
    {"title":"Spider man", "id":101, "rank":2, "rating":1.0},
    {"title":"Harry Potter", "id":102, "rank":3, "rating":6.0},
    {"title":"Flash", "id":103, "rank":4, "rating":4.0},
    {"title":"The Arrow", "id":104, "rank":5, "rating":8.0},
    {"title":"The Arrow 2.0", "id":105, "rank":5, "rating":8.0}
]

app.get('/api/movies', (req, res) => {
    res.send(movies)
  })

app.get('/api/movies/:id', (req, res) => {

    for(let movie of movies)
    {
        if(movie.id == req.params.id)
            return res.send(movie);
    }
    res.send({
        "status":400,
        "error_description": `No movie found with the id ${req.params.id}`
    })
})

app.delete('/api/movies/:id', (req, res) => {
    for(let movie of movies)
    {
        if(movie.id == req.params.id){
            movies = Object.values(movies).filter(movie=>{
                return movie.id != req.params.id;
            })
            return res.send({
                "status":200,
                "msg":`The movie ${req.params.id} was successful removed`
            })
        }
    }

    res.send({
        "status":400,
        "error_description": `No movie found with the id ${req.params.id}`
    })
})


  
  app.listen(port, () => {
    console.log(`Example app listening on port ${port}`)
  })

