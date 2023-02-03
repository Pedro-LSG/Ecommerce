# Ecommerce
#### Projeto visa reproduzir um ecommerce funcional.

Funcionalidades: cadastro de produtos, cadastro de cupons de desconto,
venda completamente funcional (exceto integração com ambiente de pagamento), carrinho de compras e cadastro de usuário com níveis de acesso diferentes [^1].

Tecnologias e conhecimentos aplicados:
- Construído com .net6 e c#.
- Utilização da arquitetura de microserviços.
- Docker para virtualização do RabbitMq.
- Mensageria com RabbitMQ para gerenciar os pedidos feitos e os pagamentos.
- Autenticacação construída com OpenId e OAuth2.
- Orquestração dos serviços utilizando Ocelot.

----

#### Autor

Pedro Luis dos Santos Gomes. | [Linkedin](https://www.linkedin.com/in/pedrogomesdev/) | Contato: pedroluis20202@gmail.com


[^1]: Usuário administrador pode cadastrar e remover produtos, informar quantidade em estoque, alterar imagem do produto 
e os usuários comuns podem utilizar-se do sistema normalmente mas somente para compras.
