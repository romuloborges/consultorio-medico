# Consultório Médico

## Introdução
Foi considerado um cenário em que o consultório pode ter mais de um médico e que o consultório não funciona aos sábados e domingos. Portanto, para agendar uma consulta com um médico ele deve estar registrado na base de dados. As consultas só podem ser agendadas para os médicos que se encontram "ativados". Vale ressaltar que ao desativar um médico, caso este não possua agendamentos registrados, ele também será excluído da base de dados. Caso este médico possua agendamento, o seu usuário será removido, mas o seu registro continuará na base de dados, porém como "Desativado".
Uma atendente, por outro lado, ao ser excluída tem seu usuário e registro excluídos.

## Instruções iniciais
Um usuário administrador será inserido no banco de dados quando as migrations forem executadas. Seu email é: admin@email.com e sua senha é 12345678. Este usuário é usado para cadastrar os médicos e as atendentes. Foi considerada uma idade mínina de 18 anos no campo de data de nascimento para a atendente e o médico.
* Não é permitido o cadastro de atendentes com CPF ou RG que alguma outra atendente já esteja usando.
* Não é permitido o cadastro de médicos com CRM, CPF ou RG que já se encontra em uso por algum outro médico.


## Tela de agendamentos da data atual
Ao realizar login com uma atendente, todas as consultas agendadas para o dia de hoje serão exibidas. Caso o login seja feito com um médico, somente as consultas agendadas para ele serão exibidas.
Nesta tela o médio atualmente logado no sistema pode registrar os atendimentos. Para tal, existe um ícone na última coluna da tabela que o leva para o registro. Para registrar uma consulta, o médico deve informar a receita médica, isto é, o que foi passado ao paciente durante a consulta; ele deve também cronometrar a duração da consulta. A consulta pode ser cronometrada através do marcador de tempo que se encontra no lado direito da tela.

## Tela de agendamento de consulta
Para o agendamento de uma consulta o usuário deverá informar a data para a qual a consulta será marcada; o paciente para a consulta; o médico para qual o agendamento está sendo feito; a hora que será agendada e as observações feitas durante o agendamento. Vale ressaltar que se o agendamento for feito para a data atual, um horário que já passou não será válido.

## Tela de listagem de agendamentos
Nesta tela são exibidos todas as consultas que ainda não foram atendidas. Esses agendamentos podem ser filtrados por um intervalo de datas, por paciente ou por médico. Aos agendamentos encontrados são permitidas as seguintes operações: visualização; edição; exclusão.

## Tela de cadastro de paciente
Para o cadastro de um paciente o usuário deverá informar os seguintes campos: 
* Nome; 
* Nome social(não obrigatório);
* Data de nascimento;
* Sexo;
* CPF;
* RG;
* Celular;
* E-mail. 
Para o cadastro de seu endereço deverá ser informado o CEP. Ao informar o CEP, as informações referentes ao CEP serão obtidas e carregadas; todos campos que não puderem ser obtidos ficarão editáveis ao usuário, para que este o preencha.

## Tela de listagem de pacientes
Nesta tela são exibidos todos os pacientes cadastrados no sistema. Esses pacientes podem ser filtrados por nome, CPF ou intervalo de data de nascimento. Aos pacientes encontrados são permitidas as operações de: visualização; edição; exclusão.