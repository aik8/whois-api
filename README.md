# KOW Whois

An extremely simple service that relays whois requests through the `grwhois.ics.forth.gr` service.

## **NOT ANYMORE, IT'S NOT!**

# KOW Whois API

This project has now become a bit more complicated. This is the API part, that provides a simple (yes, it is still quite simple **and does not implement any kind of security mechanism**... _yet!_) relay to the `grwhois.ics.forth.gr` service. It also has a brand new feature:
- It keeps track of the results.

## Things to do

There are a lot of things that need to be done:
- [ ] Add some authentication mechanism.
- [ ] Allow users to manage their own results.
- [ ] Make Docker deployment easy.

## **DISCLAIMER:**
At the moment this is meant to be used only in local networks. It should not be exposed to the public, as it does not incorporate any kind of security mechanism.
