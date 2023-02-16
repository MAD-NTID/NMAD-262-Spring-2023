from flask import *

app = Flask(__name__)

next_id = 106

#hard code mock db dataset
movies = [
    {"title":"Star War", "id":100, "rank":1, "rating":10.0},
    {"title":"Spider man", "id":101, "rank":2, "rating":1.0},
    {"title":"Harry Potter", "id":102, "rank":3, "rating":6.0},
    {"title":"Flash", "id":103, "rank":4, "rating":4.0},
    {"title":"The Arrow", "id":104, "rank":5, "rating":8.0},
    {"title":"The Arrow 2.0", "id":105, "rank":5, "rating":8.0}
]



@app.get('/api/movies')
def show_all_movies():
    return {
        "status":200,
        "movies":movies
    }

@app.get('/api/movies/<id>')
def find_movie_by_id(id):
    for movie in movies:
        if movie['id'] == int(id):
            return{
                "status":200,
                "movie": movie
            }
    
    return {
        "status":404,
        "error_description": f'No movie with the id {id} was found'
    }

@app.delete('/api/movies/<id>')
def delete_movie_from_collection(id):
    for movie in movies:
        if movie['id'] == int(id):
            movies.remove(movie)
            return {
                "status":200,
                "msg":f'The movie id {id} was successful removed from the collections'
            }

    return {
        "status":404,
        "error_description": f'No movie with the id {id} was found'
    }

@app.post('/api/movies')
def add_movie():
    global next_id
    movie = request.json

    #verify to ensure the parameter exist before adding
    movie['id'] = next_id
    next_id = next_id + 1
    movies.append(movie)

    return {
        "status":200,
        "movie":movie
    }

@app.put('/api/movies/<id>')
def update_movie(id):
    movie_update = request.json
    for movie in movies:
        if movie['id'] == int(id):
            movie_update['id'] = int(id)
            movie.update(movie_update)
            
            return {
                "status":200,
                "movie":movie
            }

    
    return {
        "status":400,
        "error_description": f'No movie with the id {id} was found'
    }
    

