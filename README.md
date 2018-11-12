Front-end:

- Tela de criação de receita.
- Tela de visualização de receitas.
- Tela de detalhes de uma receita.

Back-end:
- POST /receita (inicializa uma receita)
- GET /receitas (devolve todas as receitas em memória)
- GET /receitas/{id} (devolve uma receita em memória por id)
- GET /receitas/{id}/ingredientes (devolve os ingredientes de uma receita)
- GET /receitas/-/ingredientes/{id} (devolve receitas que contenham o ingrediente definido por id)
- GET /receitas/{ingrediente}
- GET /ingredientes (devolve os ingredientes disponíveis, ordenados alfabeticamente)
