Endpoints:
 
1. /WP/search?term=$1
Opis: Slanjem API zahteva na ovoj adresi dobija se kao rezultat koliko se neka rec javlja u pozitivnom kontekstu(rocks) i negativnom kontekstu(sucks) unutar Github Issues-a. Ukoliko ne postoje rezultati za datu rec unutar baze podataka onda se poziva API Github-a. Github API key je neophodan za rad.
Parametri: term(required) - rec ili fraza za koju se vrsi pretraga po Githubu
Respone Body:
{
  positiveScore: number,
  negativeScore: number,
  score: number,
  term: string
}

Zahtevi: 
	1. Neophodan .NET 8 ili novija verzija
	2. PostgreSQL

Pokretanje: Potrebno je promeniti username i password u fajlu 'appsettings.json' za bazu podataka
Nakon instalacije .NET i PostgreSQL baze, neophodno je pokrenuti iz Terminala/Powershella sledece komande:

dotnet ef database update (za pravljenje šeme baze podataka)
dotnet watch run (za pokretanje aplikacije)